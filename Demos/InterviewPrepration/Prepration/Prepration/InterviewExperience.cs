using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    class InterviewExperience
    {
        //https://leetcode.com/list/xoqag3yj/  --- 75 Questions
        //https://leetcode.com/discuss/general-discussion/458695/dynamic-programming-patterns/544912



        //Amazon
        //1.Given a quantityDict, priceDict and orderDict, you need to return the total bill and update the quantityDict. Partial fulfillment is allowed.
        //2.  count island problem given a matrix. The island could have invalid input and you could move across diagonals as well
        //3.  Implement a function which takes an artist and a song as input, updates the song play count and play the song. Also implement getTopSong function which takes an artist as an input and returns the top song

        //LP:
        //Situation where you couldn't give timely deliverable given by mangager or client. How did you handle it?
        //Situation where you got a chance to work with a completely new technology.
        //Something that you did on your own without seniors telling you anything and it's impact.
        //Situation where you received feedback from seniors or managers or client.How did you improvise?
        //Situation when some of your teammate's work was effecting your deliverable. How did you tackle that?

        //Apple
        //1. You are given a grid with random characters. You are also given a dictionary. You need to find all the words that you can create in the board. The words can occur multiple times. You can go up, down left and right in the board. Return a dictionary with words as key and the frequency as value. This was a mix of coding and design question. I was asked to write Pseudo code and then design the API. This question was vague, difficult and kept changing as the discussion moved forward
        //I solved it by creating a Trie from the dictionary and starting a DFS from every character in the grid
        //2.  I was aksed to implement a dictionary. This was a vague question and we discussed various things such as hashing function, collision resolution, load factor and dictionary expansion once it becomes too full. I implemented a Generic dictionary in C# using List
        //3. IsPalidrome function. many inserts and deletes will it take to covert a string to a Palindrome. I proposed a DP solution.

        //Facebook
        //1. Find the number of subsequences such that min(subseq) + max(subseq) == k
        //https://leetcode.com/discuss/interview-question/275785/facebook-phone-screen-count-subsets
        //https://leetcode.com/discuss/interview-question/268604/Google-interview-Number-of-subsets
        //2. IsSplitPossible => Given an array of positive integers, is it possible to split the array in two parts such that the sum of parts is equal. This is a very easy problem using prefix Sum.
        //Merge K sorted Lists => Given k sorted lists, merge them into one lists.It is a direct application of heap(priority queue)
        //3. Kth largest number in an array => Again a Heap question. I think I was distracted in this round because I missed some easy edge cases (e.g. k < 0)
        //Implement readline method using Read4k() => Not too challenging.I thought I solved it well but interviewer thought I missed a case.
        //4. Design an image download library/component/service
        //Highscalability.com

        //https://www.geeksforgeeks.org/microsofts-asked-interview-questions/

    }
}
