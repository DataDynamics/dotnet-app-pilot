using System;
using log4net;

namespace DataDynamics.Common.Utils;

public sealed class Mediator
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Mediator));

    //private static 인스턴스 객체
    private static readonly Lazy<Mediator> _instance = new(() => new Mediator());

    //private 생성자 
    private Mediator()
    {
        logger.Info("Mediator initialized...");
    }

    //public static 의 객체반환 함수
    public static Mediator Instance => _instance.Value;

    public override string ToString()
    {
        return "Mediator";
    }
}