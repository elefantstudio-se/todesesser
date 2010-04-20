package com.todesesser.deangardiner 
{
	import adobe.utils.CustomActions;
	import com.todesesser.contrib.octree.octreeEngine;
	import com.todesesser.contrib.octree.octreeMap;
	import flash.display.MovieClip;
	import com.todesesser.deangardiner.octreeView;
	import com.flashdynamix.utils.SWFProfiler;
	/*
	 * @author Dean Gardiner
	 */
	public class main extends MovieClip
	{
		var octree:octreeEngine;
		var player:playerOb;
		var enemy:enemyOb;
		var debug:debugStage;
		var hud:hudOb;
		var octview:octreeView;
		public function main() 
		{
			hud = new hudOb();
			player = new playerOb(stage, hud);
			debug = new debugStage(stage, player);
			octree = new octreeEngine(new octreeMap(1000, 1000, 4, debug));
			octview = new octreeView(octree);
			octview.x = 550;
			octview.y = 0;
			player.RegisterToEngine(octree);
			//Add Enemies
			for (var i:int = 0; i < 10; i++)
			{
				enemy = new enemyOb(hud);
				enemy.setPosition(300, i * 100);
				enemy.RegisterToEngine(octree);
				debug.addChild(enemy);
			}
			addChild(player);
			addChild(debug);
			addChild(hud);
			addChild(octview);
			SWFProfiler.init(stage, this);
		}
	}
}