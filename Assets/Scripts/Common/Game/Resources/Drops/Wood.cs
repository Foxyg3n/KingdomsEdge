using System;

public class Wood : Drop {

    public override Type GetCorrespondingJobType() {
        return typeof(Woodcutter);
    }
    
}