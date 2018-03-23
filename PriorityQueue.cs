using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chokudai
{
    class PriorityQueue<T> where T : IComparable
    {
        List<T> elements;
        public int count { get; private set; }
        public PriorityQueue()
        {
            elements = new List<T>();
            count = 0;
        }
        public T Front()
        {
            return elements[0];
        }
        public T Pop()
        {
            T ret = elements[0];
            count--;
            if (count != 0)
            {
                elements[0] = elements[count - 1];
                elements.RemoveAt(count - 1);
            }
            UpdateToLeaf(0);
            return ret;
        }
        public void Push(T val)
        {
            if (count >= elements.Count) elements.Add(val);
            else elements[count] = val;
            count++;
            UpdateToRoot(count - 1);
        }
        // 根から葉へ更新
        void UpdateToLeaf(int index)
        {
            int child = index * 2 + 1;
            if (child >= count) return;
            if ((child + 1 < count) && elements[child + 1].CompareTo(elements[child]) > 0) child += 1;
            if(elements[index].CompareTo(elements[child]) < 0)
            {
                T tmp = elements[index];
                elements[index] = elements[child];
                elements[child] = tmp;
                UpdateToLeaf(child);
            }
        }
        // 葉から根へ更新
        void UpdateToRoot(int index)
        {
            if (index == 0) return;
            int parent = (index - 1) / 2;
            if(elements[index].CompareTo(elements[parent]) > 0)
            {
                T tmp = elements[index];
                elements[index] = elements[parent];
                elements[parent] = tmp;
                UpdateToRoot(parent);
            }
        }
    }
}
