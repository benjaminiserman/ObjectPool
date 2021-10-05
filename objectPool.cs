using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : new()
{
	private List<T> inactiveObjects = new List<T>();

	public ObjectPool() {}

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

	public void Push(T objectPushed) => inactiveObjects.Add(objectPushed);
}