using UnityEngine;

namespace Extension
{
    public class LinkedListItem
    {
        public int        Key   { get; private set; }
        public GameObject Value { get; private set; }

        public LinkedListItem(int key, GameObject value)
        {
            Key = key;
            Value = value;
        }
    }
}