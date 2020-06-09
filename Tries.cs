using DataStructures;

public class Tries
{
    public void AutoSuggest()
    {
        Trie node = new Trie();
        node.Insert("car");
        node.Insert("art");
        node.Insert("articulate");
        node.Insert("artistic");
        node.Insert("artist");
        node.Insert("arts");

        var res = node.Search("arti");
    }
}