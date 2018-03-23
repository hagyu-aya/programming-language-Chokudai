using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chokudai
{
    class DequeElement<T>
    {
        public T val;
        public DequeElement<T> front, back;
        public DequeElement() { }
        public DequeElement(T _val, DequeElement<T> _front, DequeElement<T> _back)
        {
            val = _val;
            front = _front;
            back = _back;
        }
    }
    class Deque<T>
    {
        List<DequeElement<T>> elements;
        DequeElement<T> front, back;
        public int count { get; private set; }
        public Deque()
        {
            front = new DequeElement<T>();
            back = new DequeElement<T>();
            front.front = front;
            front.back = back;
            back.front = front;
            back.back = back;
            count = 0;
        }
        public T Front()
        {
            return front.back.val;
        }
        public T Back()
        {
            return back.front.val;
        }
        public T PopFront()
        {
            var deletion = front.back;
            front.back = deletion.back;
            deletion.back.front = front;
            count--;
            return deletion.val;
        }
        public T PopBack()
        {
            var deletion = back.front;
            back.front = deletion.front;
            deletion.front.back = back;
            count--;
            return deletion.val;
        }
        public void PushFront(T val)
        {
            var addition = new DequeElement<T>(val, front, front.back);
            front.back.front = addition;
            front.back = addition;
            count++;
        }
        public void PushBack(T val)
        {
            var addition = new DequeElement<T>(val, back.front, back);
            back.front.back = addition;
            back.front = addition;
            count++;
        }
    }
}
