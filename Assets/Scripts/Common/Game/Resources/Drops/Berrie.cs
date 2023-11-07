using System;

public class Berrie : Drop {

    public override Type GetCorrespondingJobType() {
        return typeof(Farmer);
    }
    
}