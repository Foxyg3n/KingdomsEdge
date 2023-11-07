using System.Collections;

namespace KingdomsEdge.Common {

    public interface IGameMode {

        IEnumerator OnStart();
        IEnumerator OnEnd();
    }

}