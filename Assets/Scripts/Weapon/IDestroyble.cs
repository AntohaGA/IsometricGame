using System;

public interface IDestroyble 
{
    public event Action<IDestroyble> DestroyThis;
}