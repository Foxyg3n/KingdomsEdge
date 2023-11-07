using System;

public class Tree : Resource {

    public override Type GetDropType() {
        return typeof(Wood);
    }
    
}
