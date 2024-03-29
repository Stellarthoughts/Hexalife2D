﻿using System;

namespace OOPT4Project.Extension
{
	public static class NameGenerator
	{
		public static string Generate(Random r, int len)
		{
			string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "z", "t", "v", "w", "x" };
			string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
			string Name = "";
			Name += consonants[r.Next(consonants.Length)].ToUpper();
			Name += vowels[r.Next(vowels.Length)].ToLower();
			int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
			while (b < len)
			{
				Name += consonants[r.Next(consonants.Length)];
				b++;
				Name += vowels[r.Next(vowels.Length)];
				b++;
			}

			return Name;
		}
	}
}
