namespace UmovCsxcad

open FSharp.Data

type internal Xml = XmlProvider<"..\Tools\Sample.lxml",
                                SampleIsList = true,
                                Global = true,
                                InferTypesFromValues = false>
