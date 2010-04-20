package com.todesesser.deangardiner 
{
	import com.todesesser.contrib.octree.octreeBase;
	import com.todesesser.contrib.octree.octreeCollision;
	import flash.display.Stage;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	/**
	 * @author Dean Gardiner
	 */
	public class playerOb extends octreeBase
	{
		private var stageReference:Stage;
		private var dbg:hudOb;
		private var ind:int;
		
		public function playerOb(s:Stage, d:hudOb)
		{
			this.OctreeClipName = "Player";
			dbg = d;
			x = (550 / 2);
			y = (400 / 2);
			stageReference = s;
			addEventListener(Event.ENTER_FRAME, loop);
			addEventListener("Collide", onCollide);
		}
		
		private function onCollide(e:octreeCollision)
		{
			gotoAndStop("hit");
		}
		
		private function loop(e:Event)
		{
			gotoAndStop("normal");
			this.UpdateOctree(e);
			dbg.setContainerText(this.OctreeContainerTier1.toString() + ":" + this.OctreeContainerTier2.toString());
			ind += 1;
		}
	}
}