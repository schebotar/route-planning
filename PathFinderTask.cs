using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var permutation = new int[checkpoints.Length];
			permutation[0] = 0;
			var permutationsList = new List<int[]>();
			MakePermutations(permutation, 1, permutationsList);

			var pathLengths = new double[permutationsList.Count];
			for (int i = 0; i < permutationsList.Count; i++)
			{
				pathLengths[i] = PointExtensions.GetPathLength(checkpoints, permutationsList[i]);
			}

			return permutationsList[Array.IndexOf(pathLengths, pathLengths.Min())];
		}

		static void MakePermutations(int[] permutation, int position, List<int[]> permutationsList)
		{
			if (position == permutation.Length)
			{
				permutationsList.Add(permutation.ToArray());
				return;
			}
			else
			{
				for (int i = 0; i < permutation.Length; i++)
				{
					var index = Array.IndexOf(permutation, i, 0, position);
					if (index != -1)
						continue;
					permutation[position] = i;
					MakePermutations(permutation, position + 1, permutationsList);
				}
			}
		}
	}
}