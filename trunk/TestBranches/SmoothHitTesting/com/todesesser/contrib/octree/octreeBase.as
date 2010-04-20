package com.todesesser.contrib.octree 
{
	import flash.display.MovieClip;
	import flash.events.Event;
	/**
	 * Extended from Objects needed Octree Type Hit Testing.
	 * 
	 * @author Dean Gardiner
	 */
	public class octreeBase extends MovieClip
	{
		var Registered:Boolean = false;
		var OctreeEngine:octreeEngine;
		public var OctreeContainerTier1:int;
		public var OctreeContainerTier2:int;
		public var OctreeClipName = "UnKnown";
		public var actualX:int = 0;
		public var actualY:int = 0;
		
		public function octreeBase() 
		{
		}
		
		public function RegisterToEngine(engine:octreeEngine)
		{
			OctreeEngine = engine;
			OctreeContainerTier1 = OctreeEngine.GetClipContainerTier1(this);
			OctreeContainerTier2 = OctreeEngine.GetClipContainerTier2(this);
			OctreeEngine.AddClip(OctreeContainerTier1, OctreeContainerTier2, this);
			Registered = true;
		}
		
		public function UpdateOctree(e:Event)
		{
			if (Registered == true)
			{
				var clips:Array = OctreeEngine.GetClips(this);
				for (var i:int = 0; i < clips.length; i++)
				{
					var iObject:octreeBase = clips[i];
					if (this.OctreeClipName != iObject.OctreeClipName)
					{
						if (hitTestObject(iObject) == true)
						{
							dispatchEvent(new octreeCollision("Collide", iObject));
						}
					}
				}
				OctreeEngine.UpdateClip(this);
			}
		}
	}
}