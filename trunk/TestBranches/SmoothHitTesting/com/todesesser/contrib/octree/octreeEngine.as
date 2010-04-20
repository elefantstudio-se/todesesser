package com.todesesser.contrib.octree 
{
	import adobe.utils.CustomActions;
	/**
	 * ...
	 * @author Dean Gardiner
	 */
	public class octreeEngine
	{
		private var containers:Array;
		private var Map:octreeMap;
		
		public function octreeEngine(map:octreeMap) 
		{
			Map = map;
			containers = new Array();
			//Create 8 Containers which have another 8 containers inside that.
			for (var i = 0; i <= 8; i++)
			{
				containers[i] = [8];
				for (var j = 0; j <= 8; j++)
				{
					containers[i][j] = new Array();
				}
			}
		}
		
		public function UpdateClip(o:octreeBase)
		{
			//Update, Set New Container Indexes on octreeBase
			var a:Array = containers[o.OctreeContainerTier1][o.OctreeContainerTier2];
			containers[o.OctreeContainerTier1][o.OctreeContainerTier2].splice(a.indexOf(o), 1);
			o.OctreeContainerTier1 = Map.GetContainerTier1(o.actualX, o.actualY);
			o.OctreeContainerTier2 = Map.GetContainerTier2(o.actualX, o.actualY, o.OctreeContainerTier1);
			containers[o.OctreeContainerTier1][o.OctreeContainerTier2].push(o);
		}
		
		public function AddClip(c:int, d:int, o:octreeBase)
		{
			containers[c][d].push(o);
		}
		
		public function GetClipContainerTier1(clip:octreeBase) : int
		{
			return Map.GetContainerTier1(clip.x, clip.y);
		}
		
		public function GetClipContainerTier2(clip:octreeBase) : int
		{
			return Map.GetContainerTier2(clip.x, clip.y, clip.OctreeContainerTier2);
		}
		
		public function GetClips(clip:octreeBase) : Array
		{
			//Create an Array of other octree's in the container
			var rArray:Array = containers[clip.OctreeContainerTier1][clip.OctreeContainerTier2];
			return rArray;
		}
		
		public function GetClipsInt(t1:int, t2:int) : Array
		{
			return containers[t1][t2];
		}
	}
}