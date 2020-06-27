using System;
using System.Collections;
using System.Collections.Generic;
namespace PokeApi.Helpers.DataStructures
{
    public class BinarySearchTree<T>
    {
        Node node;
        class Node{
            private String key;
            T value;
            public Node left = null;
            public Node right = null;
            public Node(String key, T value)
            {
                this.key = key;
                this.value = value;
            }
            public String GetKey(){ return key;}
            public T GetValue(){ return value;}
            public void SetValue(T _value)
            {
                this.value = _value;
            }
             public void SetValue(Node _node, T _value)
            {
                _node.value = _value;
            }
            public Node GetRight(){return right;}

            public Node GetLeft(){return left;}
        }

        public void Put(String key, T value){node = _Put(node, key, value);}

        private Node _Put(Node _node, String key, T value){
            if(_node == null)
            return new Node(key,value);

            int cmp = key.CompareTo(_node.GetKey());
            if(cmp < 0)
                _node.left = _Put(_node.GetLeft(),key,value);
            else if(cmp > 0)
                _node.right = _Put(_node.GetRight(),key,value);
            else
                _node.SetValue(value);
            return _node;
        }

        public T Get(String _key){return _Get(node, _key).GetValue();}

        private Node _Get(Node _node, String _key){
            Node searched = null;
            while(node != null){
                int cmp = _key.CompareTo(_node.GetKey());
                if(cmp < 0)
                    searched = _Get(_node.left,_key);
                else if(cmp > 0)
                    searched = _Get(_node.right,_key);
                else
                    break;
            }
            return searched;
        }

        private Node Min(Node _node){
            if(_node.left == null)
                return _node;
            else
                return Min(_node.left);
        }
        
        private Node DeleteMin(Node _node){
            if(_node.left == null)
                return _node.right;
            _node.left = DeleteMin(_node.left);
            return _node;
        }
        
        public void Delete(String _key){_Delete(node,_key);}
        
        private Node _Delete(Node _node, String _key){
            if(_node == null)
                return null;
            int cmp = _key.CompareTo(_node.GetKey());
            if(cmp < 0)
                _node.left = _Delete(_node.left,_key);
            else if(cmp > 0)
                _node.right = _Delete(_node.right, _key);
            else
            {
                if(_node.right == null)
                    return _node.left;
                if(_node.left == null)
                    return _node.right;
                Node _n = _node;
                _node = Min(_n.right);
                _node.right = DeleteMin(_n.right);
                _node.left = _n.left;
            }
            return _node;
        }
        
    }
}