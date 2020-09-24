using System;
using System.Collections.Generic;

namespace PokeApi.Helpers.DataStructures{
    public class LinkedList<Element>{
        Node head;
        Node current;
        int N = 0;
        class Node{
            public Element element;
            public Node next;
            public Node(Element element){
                this.element = element;
            }
        }
        
        public void AddToList(Element element){
            if(head == null){
                head = new Node(element);
                current = head.next;
            }
            else{
                current = new Node(element);
                current = current.next;
            }
        }

        public Element GetElement(Element element){
            Node searchedElement = null;
            for(Node node = head; node != null; node = node.next){
                if(node.element.Equals(element)){
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