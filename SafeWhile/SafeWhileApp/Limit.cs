using System;

namespace SafeWhileSample;

public class Limit
{
    private int _limit;

    public Limit(int limit)
    {
        _limit = limit;
    }

    public bool Next()
    {
        return _limit-- > 0;
    }

    public bool NextOrThrow()
    {
        if (_limit-- < 1)
            throw new InvalidOperationException();

        return true;
    }
}