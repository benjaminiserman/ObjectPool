using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : new()
{
	private List<T> inactiveObjects = new List<T>();

	public ObjectPool() {}

	///<summary>
	///Initialize the ObjectPool with a prexisting list of objects.
	///</summary>
	public ObjectPool(List<T> startingList)
	{
		inactiveObjects = startingList;
	}

	private T PullFromInactive()
	{
		T objectPulled = inactiveObjects[inactiveObjects.Count - 1];
		inactiveObjects.Remove(objectPulled);

		return objectPulled;
	}
	
	///<summary>
	///Get an object from the object pool if available, otherwise create a new one using the default constructor.
	///</summary>
	public T Pop()
	{
		T objectPulled;

		if (inactiveObjects.Count > 0)
		{
			objectPulled = PullFromInactive();
		}
		else objectPulled = new T();

		return objectPulled;
	}
	
	///<summary>
	///Get an object from the object pool if available, otherwise create a new one using supplied function.
	///</summary>
	public T Pop(Func<T> creationFunction)
	{
		T objectPulled;
		
		if (inactiveObjects.Count > 0)
		{
			objectPulled = PullFromInactive();
		}
		else objectPulled = creationFunction();

		return objectPulled;
	}

	///<summary>
	///Return an object to the object pool. Make sure to clear it as necessary before returning it!
	///No guarantees are made about the state of the object when popped.
	///</summary>
	public void Push(T objectPushed) => inactiveObjects.Add(objectPushed);
}
