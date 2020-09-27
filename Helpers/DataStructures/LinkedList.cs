using System;
using System.Collections.Generic;

namespace PokeApi.Helpers.DataStructures{
    public class LinkedList<Element>{
        Node head;
        int N = 0;
        class Node{
            public Element element;
            public Node next;
            public Node(Element element){
                this.element = element;
                next = null;
            }
        }
        
        public void AddToList(Element element){
            if(head == null){
                head = new Node(element);
            }
            else{
               Node tail = head;
               while(tail.next != null){
                   tail = tail.next;
               }
               tail.next = new Node(element);
            }
            N++;
        }

        public Element GetElement(string element){
            Node searchedElement = null;
            for(Node node = head; node != null; node = node.next){
                if(node.element.ToString().Equals(element)){
                    searchedElement = node;
                    break;
                }
            }
            return searchedElement.element;
        }
        
        public void PrintLinkedList(){
            for(Node node = head; node != null; node = node.next){
                Console.WriteLine(node.element.ToString());
            }
        }

    }
}