#nullable enable
using System;
using static System.Console;
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Method used to indicate a target Drug object when searching.
        
        public static bool IsTarget( Drug data ) 
        { 
            return data.Name.StartsWith( "FOSAMAX", StringComparison.OrdinalIgnoreCase ); 
        }
        
        // Method used to compare two Drug objects when sorting.
        // Return is -/0/+ for a<b/a=b/a>b, respectively.
        
        public static int Compare( Drug a, Drug b )
        {
            return string.Compare( a.Name, b.Name, StringComparison.OrdinalIgnoreCase );
        }
        
        // Method used to add a new Drug object to the linked list in sorted order.
        
        public void InsertInOrder( Drug newData )
        {
            //AddLast( newData ); // remove this line
                        
            Node? newNode = new Node ( newData );
            //Node? oldHead = Head;
            
            if ( newData == null ) throw new ArgumentNullException ( nameof ( newData ) );

            // linked list is empty 
            if ( Count == 0 ) 
            {
                Tail = newNode;
                Head = newNode;
                Count++;
                //newNode.Next = null;
            }
            
            //linked list is storing something 
            else
            {
                Node? previousNode = null;
                Node? currentNode = Head;
                
                // going through the list 
                for ( int i = 0; i < Count; i++ )
                {
                    // when a < b
                    if ( Compare ( newNode.Data, currentNode!.Data ) < 0 )
                    {
                        
                        // inserting at the beginning 
                        if ( currentNode == Head )
                        {
                            Node oldHead = Head;
                            newNode.Next = oldHead;
                            Head = newNode;
                            Count++;
                            return;
                        }
                        
                        // inserting it in the middle of the linked list 
                        else 
                        {
                            previousNode!.Next = newNode;
                            newNode.Next = currentNode;
                            Count++;
                            return;
                        }
                    }
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
                // inserting it at the end of the linked list when a > b
                Node? oldTail = Tail;
                oldTail!.Next = newNode;
                Tail = newNode;
                Count++;
            }
        }
    }
    
    static class Program
    {
        // Method to test operation of the linked list.
        
        static void Main( )
        {
            Drug[ ] drugArray = Drug.ArrayFromFile( "RXQT1503-100.txt" );
            
            LinkedList drugList = new LinkedList( );
            foreach( Drug d in drugArray ) drugList.InsertInOrder( d );
            
            WriteLine( "drugList.Count = {0}", drugList.Count );
            foreach( Drug d in drugList.ToArray( ) ) WriteLine( d );
        }
    }
}
