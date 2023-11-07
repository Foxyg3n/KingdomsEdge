using System;

public class Farmer : Worker {

    public override Type GetJobResourceType() {
        return typeof(Bush);
    }

    public override Type GetJobDropType() {
        return typeof(Berrie);
    }

}
