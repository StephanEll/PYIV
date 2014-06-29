using System;

namespace PYIV.Helper
{
	public class IndianSayings
	{
		/// Indian Sayings
		public static readonly string[] ALL_SAYINGS = {
			"It is better to have less thunder in the mouth and more lightning in the hand.",
			"Don't be afraid to cry. It will free your mind of sorrowful thoughts.",
			"If we wonder often, the gift of knowledge will come.",
			"Those that lie down with dogs, get up with fleas.",
			"Those who have one foot in the canoe and one foot in the boat are going to fall into the river.",
			"The weakness of the enemy makes our strength.",
			"We will be known forever by the tracks we leave.",
			"There is nothing as eloquent as a rattlesnakes tail.",
			"Force, no matter how concealed, begets resistance.",
			"Man's law changes with his understanding of man. Only the laws of the spirit remain always the same.",
			"There is no death, only a change of worlds.",
			"Life is not separate from death. It only looks that way.",
			"One finger cannot lift a pebble.",
			"Beware of the man who does not talk and the dog that does not bark.",
			"If a man is as wise as a serpent, he can afford to be as harmless as a dove.",
			"When a man moves away from nature his heart becomes hard.",
			"A brave man dies but once, a coward many times.",
			"Seek wisdom, not knowledge. Knowledge is of the past, Wisdom is of the future.",
			"A good chief gives, he does not take.",
			"Coyote is always out there waiting, and Coyote is always hungry.",
			"Listening to a liar is like drinking warm water.",
			"Every animal knows more than you do.",
			"When a fox walks lame, the old rabbit jumps.",
			"A starving man will eat with the wolf.",
			"The coward shoots with shut eyes.",
			"It is easy to be brave from a distance.",
			"Make my enemy brave and strong, so that if defeated, I will not be ashamed.",
			"White men have too many chiefs.",
			"Man has responsibility, not power." 
		};

		public static string GetSaying() {
			Random r = new Random();
			int saying_nr = r.Next(0,29);

			return ALL_SAYINGS[saying_nr];
		}
	}
}

