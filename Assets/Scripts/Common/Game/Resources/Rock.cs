using System;

public class Rock : Resource {

    public override Type GetDropType() {
        return typeof(Stone);
    }
    
}
