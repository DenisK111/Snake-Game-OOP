using Snake_Game_OOP.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game_OOP
{
    public class Direction
    {
       
        private ConsoleKeyInfo currentDirection;

        public Direction()
        {
            currentDirection = GlobalConstants.initialDirection;
        }

        public ConsoleKeyInfo CurrentDirect => currentDirection;

        public bool Move(Body body,IGameEnd gameEnd,IPauser pauser)
        {
            var prevDirection = currentDirection;

            if (Console.KeyAvailable)
            {
                currentDirection = Console.ReadKey(true);
                
                if (currentDirection.Key == ConsoleKey.Escape)
                {
                    gameEnd.End();
                    return true;
                    
                }

                if (currentDirection.Key == ConsoleKey.Spacebar)
                {
                    pauser.Pause();
                    
                    

                }


                checkDirections(prevDirection);
                             

                

            }
                moveOnce(body, body.BodyOutput.Last);
           
            return false;
        }
        private void checkDirections(ConsoleKeyInfo prevDirection)
        {
            var isValidDirection = GlobalConstants.activeDirections.Contains(currentDirection.Key);
            if (!isValidDirection)
            {
                currentDirection = prevDirection;
            }

            if (prevDirection.Key == ConsoleKey.RightArrow && currentDirection.Key == ConsoleKey.LeftArrow)
            {
                currentDirection = prevDirection;
            }

            else if (prevDirection.Key == ConsoleKey.UpArrow && currentDirection.Key == ConsoleKey.DownArrow)
            {
                currentDirection = prevDirection;
            }
            else if (prevDirection.Key == ConsoleKey.LeftArrow && currentDirection.Key == ConsoleKey.RightArrow)
            {
                currentDirection = prevDirection;
            }
            else if (prevDirection.Key == ConsoleKey.DownArrow && currentDirection.Key == ConsoleKey.UpArrow)
            {
                currentDirection = prevDirection;
            }


        }
        private void moveOnce(Body body,LinkedListNode<LinkedListNode<IDot>> currNode)
        {
                        

            while (currNode.Previous != null)
            {
                currNode.Value.Value.X = currNode.Previous.Value.Value.X;
                currNode.Value.Value.Y = currNode.Previous.Value.Value.Y;
                currNode = currNode.Previous;


            }

            if (!CheckForBugLocatationAndMove(currNode))
            {
                if (currentDirection.Key == ConsoleKey.UpArrow)
                {
                    currNode.Value.Value.Y = currNode.Value.Value.Y == GlobalConstants.upperBorder ? GlobalConstants.lowerBorder - 1 : --currNode.Value.Value.Y;

                }

                else if (currentDirection.Key == ConsoleKey.DownArrow)
                {

                    currNode.Value.Value.Y = currNode.Value.Value.Y == GlobalConstants.lowerBorder - 1 ? GlobalConstants.upperBorder : ++currNode.Value.Value.Y;
                }


                else if (currentDirection.Key == ConsoleKey.LeftArrow)
                {
                    currNode.Value.Value.X = currNode.Value.Value.X == GlobalConstants.leftBorder ? GlobalConstants.rightBorder - 1 : --currNode.Value.Value.X;
                }

                //edge case regarding bug on the lower right border point
                /* else if (currentDirection.Key == ConsoleKey.RightArrow && currNode.Value.Value.X == GlobalConstants.rightBorder - 2 && currNode.Value.Value.Y == GlobalConstants.lowerBorder - 1)
                 {

                 }*/

                else if (currentDirection.Key == ConsoleKey.RightArrow)
                {
                    currNode.Value.Value.X = currNode.Value.Value.X == GlobalConstants.rightBorder - 1 ? GlobalConstants.leftBorder : ++currNode.Value.Value.X;

                }
            }

            // if (currNode.Value.Value.X == GlobalConstants.rightBorder - 1 && currNode.Value.Value.Y == GlobalConstants.lowerBorder - 1)
            //{
            //     Console.Clear();
            //}

          

                         

            
          
        }

        // windows console is bugged when snake head goes to lower right border, that is why this field is off limits
        private bool CheckForBugLocatationAndMove (LinkedListNode<LinkedListNode<IDot>> currNode)
        {
            
            if ( currentDirection.Key == ConsoleKey.RightArrow && currNode.Value.Value.X == GlobalConstants.rightBorder - 2 && currNode.Value.Value.Y == GlobalConstants.lowerBorder - 1)
            {
                currNode.Value.Value.X = GlobalConstants.leftBorder;
                return true;
            }

            else if (currentDirection.Key == ConsoleKey.DownArrow && currNode.Value.Value.X == GlobalConstants.rightBorder - 1 && currNode.Value.Value.Y == GlobalConstants.lowerBorder - 2)
            {
                currNode.Value.Value.Y = GlobalConstants.upperBorder;
                return true;
            }

            else if (currentDirection.Key == ConsoleKey.LeftArrow && currNode.Value.Value.X == GlobalConstants.leftBorder && currNode.Value.Value.Y == GlobalConstants.lowerBorder - 1)
            {
                currNode.Value.Value.X = GlobalConstants.rightBorder-2;
                return true;
            }

            else if (currentDirection.Key == ConsoleKey.UpArrow && currNode.Value.Value.X == GlobalConstants.rightBorder-1 && currNode.Value.Value.Y == GlobalConstants.upperBorder)
            {
                currNode.Value.Value.Y = GlobalConstants.lowerBorder-2;
                return true;
            }



            return false;
        }



    }
}