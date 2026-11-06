namespace Backend;

public class DoublyLinkedList<T> where T : IComparable<T>
{
    private DoubleNode<T>? _head;
    private DoubleNode<T>? _tail;
    private bool _isDescending;

    public DoublyLinkedList()
    {
        _head = null;
        _tail = null;
        _isDescending = false;
    }
    public void Add(T data)
    {
        var newNode = new DoubleNode<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        var current = _head;
        while (current != null && !ShouldInsertBefore(data, current.Data!))
        {
            current = current.Next;
        }

        if (current == null)
        {
            _tail!.Next = newNode;
            newNode.Prev = _tail;
            _tail = newNode;
            return;
        }

        if (current.Prev == null)
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
            return;
        }

        newNode.Prev = current.Prev;
        newNode.Next = current;
        current.Prev.Next = newNode;
        current.Prev = newNode;
    }
    private bool ShouldInsertBefore(T data, T nodeData)
    {
        var comparison = data.CompareTo(nodeData);
        return _isDescending ? comparison > 0 : comparison < 0;
    }
    public void SortDescending()
    {
        if (_isDescending)
        {
            return;
        }

        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            current.Next = current.Prev;
            current.Prev = next;
            current = next;
        }

        (_head, _tail) = (_tail, _head);
        _isDescending = true;
    }
    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.CompareTo(data) == 0)
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }    
    override public string ToString()
    {
        if (_head == null)
        {
            return "Empty list";
        }
        var output = string.Empty;
        var current = _head;
        while (current != null)
        {
            output += $"{current.Data} -> ";
            current = current.Next;
        }
        return $"{output} null";
    }
    public string ToStringReverse()
    {
        if (_head == null)
        {
            return "Empty list";
        }
        var output = string.Empty;
        var current = _tail;
        while (current != null)
        {
            output += $"{current.Data} <-> ";
            current = current.Prev;
        }
        return $"{output} null";
    }
}
