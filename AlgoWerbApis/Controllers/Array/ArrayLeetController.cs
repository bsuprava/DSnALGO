using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArrayAlgoApis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArrayLeetController : ControllerBase
    {
        /* Given an integer array nums, return an array answer such that answer[i] is equal to the product of all the elements of nums except nums[i].

            The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer.

            You must write an algorithm that runs in O(n) time and without using the division operation.

            Example 1:

            Input: nums = [1,2,3,4]
            Output: [24,12,8,6]
        
        */
        [HttpPost]
        [Route("GetProductExceptSelfIndex")]
        public async Task<IActionResult> CalcutateProductOfArrayItemsExceptCurrentIndex([FromBody] int[] nums)
        {
            try
            {
                int n = nums.Length;
                int[] answer = new int[n];

                //Step 1: Calculate prefix products
                int prefix = 1;
                for (int i = 0; i < n; i++)
                {
                    answer[i] = prefix;
                    prefix *= nums[i];
                }

                // Step 2:  Calculate suffix products and multiply with prefix products
                int suffix = 1;
                for (int i = n - 1; i >= 0; i--)
                {
                    answer[i] *= suffix;
                    suffix *= nums[i];
                }
                ////Get product except self using division
                //answer = ProductExceptSelf(nums);

                return StatusCode(200, answer);



                /*
                 Time Complexity: O(n), where n is the number of elements in the array. This is because we only iterate through the array once.

                 Space Complexity: O(n), since we are storing at most n elements in the dictionary.
                */

                #region OtherSol1
                //// Step 1: Calculate prefix products
                //int[] prefixProducts = new int[n]; prefixProducts[0] = 1; 
                //for (int i = 1; i < n; i++) 
                //{ 
                //    prefixProducts[i] = prefixProducts[i - 1] * nums[i - 1]; 
                //}

                //// Step 2: Calculate suffix products and combine with prefix products 
                //int[] suffixProducts = new int[n]; 
                //suffixProducts[n - 1] = 1; 

                //for (int i = n - 2; i >= 0; i--) 
                //{ suffixProducts[i] = suffixProducts[i + 1] * nums[i + 1]; }

                //for (int i = 0; i < n; i++) 
                //{ answer[i] = prefixProducts[i] * suffixProducts[i]; }
                #endregion

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get product except self using division
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private int[] ProductExceptSelf(int[] nums)
        {
            int[] result = new int[nums.Length];

            int zeros = 0;
            int zeroIndex = 0;
            int product = 1;
            for (int i = 0; i < nums.Length; i++)
            {

                if (nums[i] == 0)
                {
                    zeroIndex = i;
                    zeros++;
                    if (zeros >= 2) break;
                }
                else product *= nums[i];
            }

            if (zeros == 1)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (i != zeroIndex)
                        result[i] = 0;
                    else
                        result[i] = product;
                }
            }
            else if (zeros == 0)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    result[i] = product / nums[i];
                }
            }
            return result;
        }

        /* Given an integer array nums, move all 0's to the end of it while maintaining the relative order of the non-zero elements.

            Note that you must do this in-place without making a copy of the array.

            Example 1:

            Input: nums = [0,1,0,3,12]
            Output: [1,3,12,0,0]
         */

        [HttpPost]
        [Route("MoveZerosToEnd")]
        public async Task<IActionResult> MoveZerosToEndOfArray([FromBody] int[] arr)
        {
            try
            {

                int n = arr.Length;
                int nonZeroIndex = 0;
                
                // Pointer to track the position for next non-zero element
                int count = 0;

                for (int i = 0; i < arr.Length; i++)
                {

                    // If the current element is non-zero
                    if (arr[i] != 0)
                    {

                        // Swap the current element with the 0 at index 'count'
                        int temp = arr[i];
                        arr[i] = arr[count];
                        arr[count] = temp;

                        // Move 'count' pointer to the next position
                        count++;
                    }

                    //// Move all non-zero elements to the front
                    //for (int i = 0; i < n; i++)
                    //{
                    //    if (nums[i] != 0)
                    //    {
                    //        nums[nonZeroIndex] = nums[i];
                    //        nonZeroIndex++;
                    //    }
                    //}

                    //// Fill the remaining positions with zeroes
                    //for (int i = nonZeroIndex; i < n; i++)
                    //{
                    //    nums[i] = 0;
                    //}

                   
                }
                return StatusCode(200, arr);
                /*
               Time Complexity: O(n), Both passes iterate through the array once

               Space Complexity: O(1), array is used to store results, making the space complexity O(1) for auxiliary space beyond the output.
              */
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        /* You are given an array prices where prices[i] is the price of a given stock on the ith day.

        You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.

        Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.

        Example 1:

        Input: prices = [7,1,5,3,6,4]
        Output: 5
        Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.
        */

        [HttpPost]
        [Route("GetBestTimeToBuySellStock")]
        public async Task<IActionResult> GetBestTimeToBuySellStock([FromBody] int[] prices)
        {
            try
            {
                if (prices == null || prices.Length < 2)
                    return StatusCode(400, 0);

                int minPrice = int.MaxValue;
                int maxProfit = 0;

                foreach (int price in prices)
                {
                    if (price < minPrice)
                    {
                        minPrice = price;
                    }
                    else if (price - minPrice > maxProfit)
                    {
                        maxProfit = price - minPrice;
                    }
                }
                return StatusCode(200, maxProfit);
                /*
               Time Complexity: O(n), Both passes iterate through the array once

               Space Complexity: O(1), array is used to store results, making the space complexity O(1) for auxiliary space beyond the output.
              */
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
