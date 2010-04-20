package com.todesesser.deangardiner 
{
	import flash.display.MovieClip;
	import flash.display.Stage;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	/*
	 * @author Dean Gardiner
	 */
	public class debugStage extends MovieClip
	{
		private var stageReference:Stage;
		private var playerRef:playerOb;
		private var playerSpeed:int = 10;
		public function debugStage(s:Stage, p:playerOb) 
		{
			x = (550 / 2);
			y = (400 / 2);
			playerRef = p;
			stageReference = s;
			stageReference.addEventListener(KeyboardEvent.KEY_DOWN, onKeyDownEvent);
		}
		private function onKeyDownEvent(e:KeyboardEvent)
		{
			switch(e.keyCode)
			{
				case 37:
					x += playerSpeed;
					playerRef.actualX -= playerSpeed;
					break;
				case 38:
					y += playerSpeed;
					playerRef.actualY -= playerSpeed;
					break;
				case 39:
					x -= playerSpeed;
					playerRef.actualX += playerSpeed;
					break;
				case 40:
					y -= playerSpeed;
					playerRef.actualY += playerSpeed;
					break;
			}
		}
	}
}