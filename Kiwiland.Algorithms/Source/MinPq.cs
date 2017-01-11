using System;
using System.Collections.Generic;

namespace Kiwiland.Algorithms
{
    /// <summary>
    /// Array based Min Priority Queue implementation based on min heap.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinPQ<T> where T : IComparable<T>
    {
        #region Fields

        /// <summary>
        /// The items in the priority queue.
        /// </summary>
        List<T> _items = new List<T>();

        #endregion

        #region Public MinPQ API

        /// <summary>
        /// Inserts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Insert(T key)
        {
            _items.Add(key);
            SiftUp(_items.Count - 1);
        }

        /// <summary>
        /// Deletes the minimum key from PQ.
        /// </summary>
        /// <returns></returns>
        public T DelMin()
        {
            var top = _items[0];

            _items[0] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            MinHeapify(_items, 0, _items.Count - 1);

            return top;
        }

        /// <summary>
        /// Determines whether PQ is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /// <summary>
        /// The min element in the PQ.
        /// </summary>
        /// <returns></returns>
        public T Min()
        {
            return _items[0];
        }

        /// <summary>
        /// Determines whether PQ contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if contains the specified key; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T key)
        {
            return _items.Contains(key);
        }

        /// <summary>
        /// Size of the PQ.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return _items.Count;
        }

        /// <summary>
        /// Changes the key value in the PQ.
        /// </summary>
        /// <param name="oldKey">The old key.</param>
        /// <param name="newKey">The new key.</param>
        public void ChangeKey(T oldKey, T newKey)
        {
            if (_items.Contains(oldKey))
            {
                var index = _items.IndexOf(oldKey);
                _items[index] = newKey;
                MinHeapify(_items, 0, _items.Count - 1);
            }
        }

        #endregion

        #region Maintain min heap helpers

        /// <summary>
        /// Sifts up the element at given index to it corrects position in the heap 
        /// and maintain min heap properties.
        /// </summary>
        /// <param name="index">The index.</param>
        private void SiftUp(int index)
        {
            if (index > 0)
            {
                if (_items[index / 2].CompareTo(this._items[index]) == 1)
                {
                    var temp = this._items[index / 2];
                    _items[index / 2] = this._items[index];
                    _items[index] = temp;
                    SiftUp(index / 2);
                }
            }

        }

        /// <summary>
        /// Heapify the PQ to place elements at their correct indices and maintain min heap properties.
        /// </summary>
        /// <param name="heapItems">The heap items.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        private void MinHeapify(List<T> heapItems, int index, int size)
        {
            var left = 2 * index;
            var right = 2 * index + 1;

            if (left <= size && heapItems[index].CompareTo(heapItems[left]) == 1)
            {
                var temp = heapItems[index];
                heapItems[index] = heapItems[left];
                heapItems[left] = temp;
                MinHeapify(heapItems, left, size);
            }

            if (right <= size && heapItems[index].CompareTo(heapItems[right]) == 1)
            {
                var temp = heapItems[index];
                heapItems[index] = heapItems[right];
                heapItems[right] = temp;
                MinHeapify(heapItems, right, size);
            }
        }

        #endregion

    }
}
