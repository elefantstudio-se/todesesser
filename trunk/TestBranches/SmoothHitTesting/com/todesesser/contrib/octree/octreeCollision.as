package com.todesesser.contrib.octree 
{
	import flash.events.Event;
	/*
	 * @author Dean Gardiner
	 */
	public class octreeCollision extends Event
	{
		public var CollidedWith:octreeBase;
		public function octreeCollision(type:String, parent:octreeBase) 
		{
			this.CollidedWith = parent;
			super(type, false, false);
		}
	}
}