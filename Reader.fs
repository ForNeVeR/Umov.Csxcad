module Tesla.Models.Reader

open FSharp.Data

type CSXCAD = XmlProvider<".\Tools\CSX.xml">
