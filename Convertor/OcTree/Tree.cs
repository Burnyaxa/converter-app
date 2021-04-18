using Converter.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Converter.OcTree
{
	public class Tree
	{
		private float maxSize;
		private float minSize;
		private TreeNode head;
		Tree(float max, List<Triangle> allTriangles)
		{
			minSize = max / 10;
			maxSize = max;
			head = new TreeNode(-max, max, -max, max, -max, max);
			head.Triangles = allTriangles;
			head.Rebuild(minSize);
		}

		void FindIntersections(Vector3 rayOrigin, Vector3 rayVector, List<Triangle> result)
		{
			head.FindIntesections(rayOrigin, rayVector, result);
		}
	}
}
