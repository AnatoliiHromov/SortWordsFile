module SortLargeFile

open System.IO

let readAndSortFiles (file:string, num:int): int= 
    use sr = new StreamReader (file)
    let mutable valid = true
    let mutable c = 0
    while (valid) do
        let mutable count = 0
        let lst = List.ofSeq(seq{
            while(valid && count < num) do   
                let line = sr.ReadLine()
                if (line = null) then
                    valid <- false
                else if(line = "")then
                    valid<-true                   
                else
                    count<-count+1
                    yield line})
        let sorted = List.sort lst
        use sw = new StreamWriter ("tmp"+c.ToString()+".txt")
        for s in sorted do
            sw.WriteLine(s)
        sw.Close()
        c<-c+1
    c



let LinebyIndex (file:string,index:int)=
    use sr = new StreamReader (file)
    let file = sr.ReadToEnd().Split('\n')
    let mutable line = ""
    if (file.Length >=index+1) then 
        line<-Array.get file index
    line

let GetHeadLines (count: int, pos:int)= List.ofSeq(seq{
    for n in 0..count do
        let line = LinebyIndex ("tmp"+n.ToString()+".txt",pos)
        if(line <> "") then
            yield line})

let SortLargeFile (file, num, resfile)=
    let c = readAndSortFiles (file, num)-1
    let mutable pos =0
    let mutable valid = true
    let list = GetHeadLines(c,pos)
    let mutable resw = List.sort list
    pos<-pos+1
    while(valid) do 
        let list = GetHeadLines(c,pos) 
        let sorted = List.sort list
        pos<-pos+1
        resw <- 
            match sorted with
            |[] -> 
                use sw = new StreamWriter(resfile,true)
                for r in resw do        
                    sw.Write(r)
                sw.Close()
                valid<-false
                []
            |h::t -> 
                let smaler = 
                    resw |> List.filter(fun e -> e<h)
                let larger =
                    resw |> List.filter(fun e -> e>=h)
                use sw = new StreamWriter(resfile,true)
                for s in smaler do
                    sw.Write(s)
                sw.Close()
                List.concat [[h];larger;t]|>List.sort  
    for n in 0..c do
        if(File.Exists("tmp"+n.ToString()+".txt")) then
            File.Delete("tmp"+n.ToString()+".txt")
    pos