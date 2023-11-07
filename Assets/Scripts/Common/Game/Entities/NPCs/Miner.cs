using System;

public class Miner : Worker {

    public override Type GetJobResourceType() {
        return typeof(Rock);
    }

    public override Type GetJobDropType() {
        return typeof(Stone);
    }
    
}
