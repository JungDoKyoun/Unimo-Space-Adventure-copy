using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    private readonly List<(T item, int priority)> _elements = new List<(T item, int priority)>();

    public int Count => _elements.Count;

    public void Enqueue(T item, int priority)
    {
        _elements.Add((item, priority));
    }

    public T Dequeue()
    {
        int baseIndex = 0;
        int basePriority = _elements[0].priority;

        for(int i = 0; i < _elements.Count; i++)
        {
            if (_elements[i].priority < basePriority)
            {
                baseIndex = i;
                basePriority = _elements[i].priority;
            }
        }

        T baseItem = _elements[baseIndex].item;
        _elements.RemoveAt(baseIndex);
        return baseItem;
    }
}
