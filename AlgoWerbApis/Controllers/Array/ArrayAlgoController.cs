using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArrayAlgoApis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArrayAlgoController: ControllerBase
    {
        /*
         * Case1:
         Input
                nums = [2,7,11,15]
                target = 9
         Output
                [0,1]

        Case2:
        Input
                nums = [2,10,11,5]
                target = 15
         Output
                [1,3]

        
        Case3:
        Input
                nums = [3,3]
                target = 6
         Output
                [0,1]
        
        */
        [HttpPost]
        [Route("FindNumsToTargetSum")]
        public async Task<IActionResult> FindNumsToTargetSum([FromBody] FindNumsToTargetSumInput inputFindNumsToTargetSum )
        {
            try
            {
                
                var numDict = new Dictionary<int, int>();

                for (int i = 0; i < inputFindNumsToTargetSum.nums.Length; i++)
                {
                    int complement = inputFindNumsToTargetSum.sum - inputFindNumsToTargetSum.nums[i];
                    if (numDict.ContainsKey(complement))
                    {                       
                        return StatusCode(200, new int[] { numDict[complement], i });
                    }
                    
                    numDict[inputFindNumsToTargetSum.nums[i]] = i;
                }

                return StatusCode(500, new ArgumentException("No two sum solution"));
                /*
                 Time Complexity: O(n), where n is the number of elements in the array. This is because we only iterate through the array once.

                 Space Complexity: O(n), since we are storing at most n elements in the dictionary.
                */
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("FindMinSubArrayLen")]
        public async Task<IActionResult> FindMinSubArrayLen([FromBody] FindNumsToTargetSumInput inputFindNumsToTargetSum)
        {
            int result = MinSubArrayLen(inputFindNumsToTargetSum.sum, inputFindNumsToTargetSum.nums);

            if (result > 0)
            {
                string success = $"The minimal length of a subarray with sum >= {inputFindNumsToTargetSum.sum} is {result}.";
                return StatusCode(200, success);
            }
            else
            {
                string error = "No subarray meets the condition.";
                return StatusCode(400, error);
            }
            /*
            Input: {
                      "nums": [
                        12, 3, 2, 6, 4, 6 
                      ],
                      "sum": 12
                    }
            output: 1
             Time Complexity: O(n) 
            Space Complexity: O(1)
             */

        }
        //uses sliding window method
        private int MinSubArrayLen(int target, int[] nums)
        {
            int n = nums.Length;
            int minLength = int.MaxValue;
            int left = 0;
            int currentSum = 0;

            for (int right = 0; right < n; right++)
            {
                currentSum += nums[right];

                while (currentSum >= target)
                {
                    minLength = Math.Min(minLength, right - left + 1);
                    currentSum -= nums[left];
                    left++;
                }
            }

            return minLength == int.MaxValue ? 0 : minLength;
        }

        public class FindNumsToTargetSumInput
        {
            public int[] nums { get; set; }
            public int sum { get; set; }
        }        
        private int[] GetBy2PointerMethod(int[] nums, int target)
        {
            // Create a copy of the original array to keep the original indices
            var numsWithIndices = nums.Select((num, index) => new { Num = num, Index = index }).ToArray();

            // Sort the copy based on the values
            Array.Sort(numsWithIndices, (a, b) => a.Num.CompareTo(b.Num));

            // Two-pointer technique
            int left = 0; 
            int right = numsWithIndices.Length - 1;

            while (left < right) { 

                int sum = numsWithIndices[left].Num + numsWithIndices[right].Num; 
                if (sum == target) 
                { return new int[] { numsWithIndices[left].Index, numsWithIndices[right].Index }; } 
                else if (sum < target) { left++; } 
                else { right--; } 
            }

            return Array.Empty<int>();
            /*
             Time Complexity: O(n log n) due to the sorting step.

             Space Complexity: O(n) for the additional array storing values and their indices.
             */

        }

        [HttpPost]
        [Route("FindSubString")]
        public async Task<IActionResult> FindSubString([FromBody] FindSubStringInput findSubStringInput)
        {
            int index = GetSubstring(findSubStringInput.OriginalText, findSubStringInput.SearchText);

            if (index != -1)
            {
                string success = $"Substring '{findSubStringInput.SearchText}' found at index {index}.";
                return StatusCode(200, success);
            }
            else
            {
               string error = $"Substring '{findSubStringInput.SearchText}' not found in the string.";
                return StatusCode(400, error);
            }
            /*
             Time Complexity: O((n−m+1)⋅m) = O(n⋅m) 
            Space Complexity: O(m)
             */
        }

        private int GetSubstring(string str, string target)
        {
            if (str.Length < target.Length)
                return -1;

            int windowSize = target.Length;
            int n = str.Length;

            for (int i = 0; i <= n - windowSize; i++)
            {
                // Extract the current window substring
                string window = str.Substring(i, windowSize);

                // Check if it matches the target
                if (window == target)
                {
                    return i;
                }
            }

            // Substring not found
            return -1;
        }

        public class FindSubStringInput
        {
            public string OriginalText { get; set; }
            public string SearchText { get; set; }
        }
        
    }
}
