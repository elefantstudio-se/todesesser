package com.todesesser.deangardiner 
{
	import com.todesesser.contrib.octree.octreeBase;
	import com.todesesser.contrib.octree.octreeCollision;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	/**
	 * @author Dean Gardiner
	 */
	public class enemyOb extends octreeBase
	{
		private var dbg:hudOb;
		private var dir:String = "right";
		public function enemyOb(d:hudOb) 
		{
			this.x = 600;
			this.y = 600;
			this.actualX = 600;
			this.actualY = 600;
			dbg = d;
			this.OctreeClipName = "Enemy";
			addEventListener(Event.ENTER_FRAME, enterFrame);
			addEventListener("Collide", onCollide);
		}
		
		public function setPosition(X:int, Y:int)
		{
			this.x = X;
			this.y = Y;
			this.actualX = X;
			this.actualY = Y;
		}
		
		private function onCollide(e:octreeCollision)
		{
			
		}
		
		private function enterFrame(e:Event)
		{
			this.UpdateOctree(e);
			dbg.setEnemyText(this.OctreeContainerTier1.toString() + ":" + this.OctreeContainerTier2.toString());
			switch(dir)
			{
				case "right":
					this.x += 1;
					this.actualX += 1;
					break;
				case "left":
					this.x -= 1;
					this.actualX -= 1;
					break;
			}
			if (this.x >= 900)
			{
				dir = "left";
			} else if (this.x <= 600)
			{
				dir = "right";
			}
		}
	}
}