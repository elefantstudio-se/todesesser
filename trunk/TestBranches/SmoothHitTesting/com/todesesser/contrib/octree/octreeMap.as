package com.todesesser.contrib.octree 
{
	import adobe.utils.CustomActions;
	import com.todesesser.contrib.octree.octreeContainer;
	import com.todesesser.deangardiner.debugStage;
	import flash.display.InterpolationMethod;
	import com.todesesser.deangardiner.pointOb;
	/**
	 * @author Dean Gardiner
	 */
	public class octreeMap
	{
		private var containers:Array;
		private var containersTier2:Array;
		private var dbg:debugStage;
		private var tier3size:int;
		
		public function octreeMap(width:int, height:int, splitSize:int, debug:debugStage) 
		{
			dbg = debug;
			containers = new Array();
			containersTier2 = new Array();
			//Split the Map up into containers
			var dSplit = height / (splitSize / 2);
			var rSplit = width / (splitSize / 2);
			
			var xPos:int = 0;
			var yPos:int = 0;
			var xPos2:int = rSplit;
			
			for (var i = 1; i <= (splitSize / 2); i++)
			{
				containers.push(new octreeContainer(xPos, yPos, rSplit, dSplit));
				containers.push(new octreeContainer(xPos2, yPos, rSplit, dSplit));
				xPos += rSplit;
				yPos += dSplit;
				xPos2 -= rSplit;
			}
			
			for (var j = 0; j < containers.length; j++)
			{
				containersTier2[j] = [splitSize];
			}
			
			//Split the Tier 2 Containers into more Containers (Tier 3 Containers)
			for (var k = 0; k < containers.length; k++)
			{
				//Split Containers and place them in 'containersTier2'
				var contain:octreeContainer = containers[k];
				var aSplit = contain.Height / (splitSize / 2);
				var bSplit = contain.Width / (splitSize / 2);
				
				var aXPos:int = 0;
				var aYPos:int = 0;
				var aXPos2:int = bSplit;
				
				var lN = 0;
				
				var parentContainer:octreeContainer = containers[k];
				
				for (var l = 0; l <= (splitSize / 2); l++)
				{
					containersTier2[k][lN] = new octreeContainer(parentContainer.X + aXPos, parentContainer.Y + aYPos, bSplit, aSplit);
					lN += 1;
					containersTier2[k][lN] = new octreeContainer(parentContainer.X + aXPos2, parentContainer.Y + aYPos, bSplit, aSplit);
					lN += 1;
					aXPos += bSplit;
					aYPos += aSplit;
					aXPos2 -= bSplit;
				}
			}
			
			for (var a = 0; a < containersTier2.length; a++)
			{
				for (var b = 0; b <= splitSize - 1; b++)
				{
					var t3container:octreeContainer = containersTier2[a][b];
					AddPoint(t3container.X, t3container.Y);
				}
			}
		}
		
		private function AddPoint(x:int, y:int)
		{
			dbg.addChild(new pointOb(x, y));
		}
		
		public function GetContainerTier1(x:int, y:int)
		{
			for (var k = 0; k < containers.length; k++)
			{
				var cont:octreeContainer = containers[k];
				if (x >= cont.X &&
					x <= cont.X + cont.Width &&
					y >= cont.Y &&
					y <= cont.Y + cont.Height)
					{
						return k;
					}
			}
			return NaN;
		}
		
		public function GetContainerTier2(x:int, y:int, t1:int)
		{
			for (var k = 0; k < containersTier2.length; k++)
			{
				var cont:octreeContainer = containersTier2[t1][k];
				if (x >= cont.X &&
					x <= cont.X + cont.Width &&
					y >= cont.Y &&
					y <= cont.Y + cont.Height)
					{
						return k;
					}
			}
			return NaN;
		}
	}
}