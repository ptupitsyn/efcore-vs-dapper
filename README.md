# EF Core vs Dapper
Simple benchmark results:

* `BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362`
* `Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores`
* `NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT`
  

|                     Method |      Mean |     Error |    StdDev |
|--------------------------- |----------:|----------:|----------:|
|         SelectEntityByIdEf | 76.171 us | 0.4737 us | 0.4199 us |
| SelectEntityByIdEfCompiled | 41.593 us | 0.6043 us | 0.5653 us |
|     SelectEntityByIdDapper |  7.749 us | 0.0482 us | 0.0451 us |
|          SelectFieldByIdEf | 83.249 us | 0.2535 us | 0.2371 us |
|  SelectFieldByIdEfCompiled | 47.315 us | 0.3554 us | 0.3325 us |
|      SelectFieldByIdDapper |  7.030 us | 0.1042 us | 0.0974 us |

