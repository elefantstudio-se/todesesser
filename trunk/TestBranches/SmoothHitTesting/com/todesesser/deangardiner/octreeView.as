package com.todesesser.deangardiner 
{
	import com.todesesser.contrib.octree.octreeEngine;
	import flash.display.MovieClip;
	import flash.events.Event;
	/*
	 * @author Dean Gardiner
	 */
	public class octreeView extends MovieClip
	{
		private var engine:octreeEngine;
		public function octreeView(octengine:octreeEngine) 
		{
			engine = octengine;
			addEventListener(Event.ENTER_FRAME, loop);
		}
		
		private function loop(e:Event)
		{
			t0t0.text = engine.GetClipsInt(0, 0).length.toString();
			t0t1.text = engine.GetClipsInt(0, 1).length.toString();
			t0t2.text = engine.GetClipsInt(0, 2).length.toString();
			t0t3.text = engine.GetClipsInt(0, 3).length.toString();
			t1t0.text = engine.GetClipsInt(1, 0).length.toString();
			t1t1.text = engine.GetClipsInt(1, 1).length.toString();
			t1t2.text = engine.GetClipsInt(1, 2).length.toString();
			t1t3.text = engine.GetClipsInt(1, 3).length.toString();
			t2t0.text = engine.GetClipsInt(2, 0).length.toString();
			t2t1.text = engine.GetClipsInt(2, 1).length.toString();
			t2t2.text = engine.GetClipsInt(2, 2).length.toString();
			t2t3.text = engine.GetClipsInt(2, 3).length.toString();
			t3t0.text = engine.GetClipsInt(3, 0).length.toString();
			t3t1.text = engine.GetClipsInt(3, 1).length.toString();
			t3t2.text = engine.GetClipsInt(3, 2).length.toString();
			t3t3.text = engine.GetClipsInt(3, 3).length.toString();
		}
	}
}