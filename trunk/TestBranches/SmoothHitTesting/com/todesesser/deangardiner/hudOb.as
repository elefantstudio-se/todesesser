package com.todesesser.deangardiner 
{
	import flash.display.MovieClip;
	/*
	 * @author Dean Gardiner
	 */
	public class hudOb extends MovieClip
	{
		public function hudOb() 
		{

		}
		
		public function setContainerText(text:String)
		{
			PlayerContainer.text = text;
		}
		
		public function setEnemyText(text:String)
		{
			EnemyContainer.text = text;
		}
	}
}