using System;

public class Stone : Drop {

    public override Type GetCorrespondingJobType() {
        return typeof(Miner);
    }
    
}