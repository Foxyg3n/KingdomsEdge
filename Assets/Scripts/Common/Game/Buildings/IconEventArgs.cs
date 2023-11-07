using System;
public class IconEventArgs : EventArgs {

    public IconID iconID;

    public IconEventArgs(IconID iconID) {
        this.iconID = iconID;
    }

}
