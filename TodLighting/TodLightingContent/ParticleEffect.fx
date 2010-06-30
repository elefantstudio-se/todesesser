//-----------------------------------------------------------------------------
// ParticleEffect.fx
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


// Camera parameters.
float4x4 View;
float4x4 Projection;
float ViewportHeight;
float Aspect; // Convert4.0


// The current time, in seconds.
float CurrentTime;


// Parameters describing how the particles animate.
float Duration;
float DurationRandomness;
float3 Gravity;
float EndVelocity;
float4 MinColor;
float4 MaxColor;


// These float2 parameters describe the min and max of a range.
// The actual value is chosen differently for each particle,
// interpolating between x and y by some random amount.
float2 RotateSpeed;
float2 StartSize;
float2 EndSize;


// Particle texture and sampler.
texture Texture;

sampler Sampler = sampler_state
{
    Texture = (Texture);
    
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Point;
    
    AddressU = Clamp;
    AddressV = Clamp;
};


// Vertex shader input structure describes the start position and
// velocity of the particle, and the time at which it was created,
// along with some random values that affect its size and rotation.
struct VertexShaderInput
{
    float3 Position : POSITION0;
    float3 Velocity : NORMAL0;
	float2 Offset : PSIZE0; // Convert4.0 - added offset to show which quad corner this is.
    float4 Random : COLOR0;
    float Time : TEXCOORD0;
};


// Vertex shader output structure specifies the position, size, and
// color of the particle, plus a 2x2 rotation matrix (packed into
// a float4 value because we don't have enough color interpolators
// to send this directly as a float2x2).
// Convert4.0 - Note that Size and Rotation have been removed, because they
// are only calculated and immediately used within the vertex shader.
// Also added a TexCoord, because we no longer have the implicit 
// texture coordinates of the PointSprite.
struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
	float2 TexCoord : TEXCOORD0; // - Convert4.0
};


// Vertex shader helper for computing the position of a particle.
float4 ComputeParticlePosition(float3 position, float3 velocity,
                               float age, float normalizedAge)
{
    float startVelocity = length(velocity);

    // Work out how fast the particle should be moving at the end of its life,
    // by applying a constant scaling factor to its starting velocity.
    float endVelocity = startVelocity * EndVelocity;
    
    // Our particles have constant acceleration, so given a starting velocity
    // S and ending velocity E, at time T their velocity should be S + (E-S)*T.
    // The particle position is the sum of this velocity over the range 0 to T.
    // To compute the position directly, we must integrate the velocity
    // equation. Integrating S + (E-S)*T for T produces S*T + (E-S)*T*T/2.

    float velocityIntegral = startVelocity * normalizedAge +
                             (endVelocity - startVelocity) * normalizedAge *
                                                             normalizedAge / 2;
     
    position += normalize(velocity) * velocityIntegral * Duration;
    
    // Apply the gravitational force.
    position += Gravity * age * normalizedAge;
    
    // Apply the camera view and projection transforms.
    return mul(mul(float4(position, 1), View), Projection);
}


// Vertex shader helper for computing the size of a particle.
float ComputeParticleSize(float4 projectedPosition,
                          float randomValue, float normalizedAge)
{
    // Apply a random factor to make each particle a slightly different size.
    float startSize = lerp(StartSize.x, StartSize.y, randomValue);
    float endSize = lerp(EndSize.x, EndSize.y, randomValue);
    
    // Compute the actual size based on the age of the particle.
    float size = lerp(startSize, endSize, normalizedAge);
    
    // Project the size into screen coordinates.
    return size * Projection._m11 / projectedPosition.w * ViewportHeight / 2;
}


// Vertex shader helper for computing the color of a particle.
float4 ComputeParticleColor(float4 projectedPosition,
                            float randomValue, float normalizedAge)
{
    // Apply a random factor to make each particle a slightly different color.
    float4 color = lerp(MinColor, MaxColor, randomValue);
    
    // Fade the alpha based on the age of the particle. This curve is hard coded
    // to make the particle fade in fairly quickly, then fade out more slowly:
    // plot x*(1-x)*(1-x) for x=0:1 in a graphing program if you want to see what
    // this looks like. The 6.7 scaling factor normalizes the curve so the alpha
    // will reach all the way up to fully solid.
    
    color.a *= normalizedAge * (1-normalizedAge) * (1-normalizedAge) * 6.7;

	// Convert4.0 - We're using PreMultiplied alpha now, which means we need to multiply
	// it into the color as well as into the alpha.
	color.rgb *= color.a;
   
    return color;
}


// Vertex shader helper for computing the rotation of a particle.
float4 ComputeParticleRotation(float randomValue, float age)
{    
    // Apply a random factor to make each particle rotate at a different speed.
    float rotateSpeed = lerp(RotateSpeed.x, RotateSpeed.y, randomValue);
    
    float rotation = rotateSpeed * age;

    // Compute a 2x2 rotation matrix.
    float c = cos(rotation);
    float s = sin(rotation);
    
    float4 rotationMatrix = float4(c, -s, s, c);
    
#if 0 /// Convert4.0 We're going to use this rotation to rotate vertices, not texture coords,
		/// so all of the following is obsolete.
    // Normally we would output this matrix using a texture coordinate interpolator,
    // but texture coordinates are generated directly by the hardware when drawing
    // point sprites. So we have to use a color interpolator instead. Only trouble
    // is, color interpolators are clamped to the range 0 to 1. Our rotation values
    // range from -1 to 1, so we have to scale them to avoid unwanted clamping.
    
    rotationMatrix *= 0.5;
    rotationMatrix += 0.5;
#endif
    
    return rotationMatrix;
}


// Custom vertex shader animates particles entirely on the GPU.
VertexShaderOutput VS_VertexShader(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    // Compute the age of the particle.
    float age = CurrentTime - input.Time;
    
    // Apply a random factor to make different particles age at different rates.
    age *= 1 + input.Random.x * DurationRandomness;
    
    // Normalize the age into the range zero to one.
    float normalizedAge = saturate(age / Duration);

    // Compute the particle position, size, color, and rotation.
    output.Position = ComputeParticlePosition(input.Position, input.Velocity,
                                              age, normalizedAge);

	// Convert4.0 - extract our uv in range [0..1] from our offset, which
	// is in range [-1..1].
	output.TexCoord = input.Offset * 0.5f.xx + 0.5f.xx;
    
    float2 size = ComputeParticleSize(output.Position, input.Random.y, normalizedAge).xx;
    output.Color = ComputeParticleColor(output.Position, input.Random.z, normalizedAge);
    float4 rotation = ComputeParticleRotation(input.Random.w, age);

	// Convert4.0 - Keep our particles from getting too small to be seen (what a waste).
	size = max(size, float2(0.75f, 0.75f));

	size = size * input.Offset;
	size = size.xx * rotation.xy + size.yy * rotation.zw;

	// Convert4.0 - Fix up the size and make them square, accounting for the aspect ratio.
	// We're currently in pixel units, but we want -1 to 1 NDC units, so divide out
	// the ViewportHeight. Then shrink the width by the aspect ratio, so they are square
	// even when the screen isn't.
	size /= ViewportHeight;
	size *= output.Position.w;
	size.x *= Aspect;
	

	output.Position.xy += size;
    
    return output;
}



// Pixel shader for drawing particles that do not rotate.
// Convert4.0 - since the rotation happens in the vertex shader now,
// our pixel shader is very simple.
float4 PS_PixelShader(VertexShaderOutput input) : COLOR0
{
    return tex2D(Sampler, input.TexCoord) * input.Color;
}



// Effect technique for drawing particles that do not rotate. Requires SM2.
// Convert4.0 - Only the one technique here, because rotation happens in
// the vertex shader.
technique Particles
{
    pass P0
    {
        VertexShader = compile vs_2_0 VS_VertexShader();
        PixelShader = compile ps_2_0 PS_PixelShader();
    }
}


