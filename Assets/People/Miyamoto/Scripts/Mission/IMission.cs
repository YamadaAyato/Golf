using System;

public interface IMission
{
    void Initialize();
    bool IsCompleted();
    void Dispose();
}
