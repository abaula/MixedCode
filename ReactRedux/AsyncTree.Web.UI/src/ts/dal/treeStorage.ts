import { ITreeNodeDto } from "./iTreeNodeDto"
import { Seq, Map, Iterable } from "immutable"
import { ITreeStorage } from "./iTreeStorage"

export default class TreeStorage implements ITreeStorage
{
    private valuesMap: Map<number, ITreeNodeDto>
    private parentsMap: Map<number, Iterable<number, ITreeNodeDto>>

    constructor()
    {
        let state = new Array<ITreeNodeDto>()

        for (var i = 1; i <= 20; i++)
        {
            state.push({
                id: i,
                level: 1,
                parentId: 0,
                text: "Node - " + i,
                hasChildren: true
            })

            for (var j = 1; j <= 20; j++)
            {
                let indexJ = 20 * i + j

                state.push({
                    id: indexJ,
                    level: 2,
                    parentId: i,
                    text: "Node - " + indexJ,
                    hasChildren: true
                })

                for (var k = 1; k <= 20; k++)
                {
                    let indexK = 20 * indexJ + k

                    state.push({
                        id: indexK,
                        level: 3,
                        parentId: indexJ,
                        text: "Node - " + indexK,
                        hasChildren: false
                    })
                }
            }
        }

        //console.log(state)
        this.loadData(state)  
    }

    private loadData = (data: ITreeNodeDto[]): void =>
    {
        this.valuesMap = Map<number, ITreeNodeDto>(
            data.map(value => [value.id, value])
        )

        this.parentsMap = Map(
            this.valuesMap
                .toKeyedSeq()
                .groupBy((value: ITreeNodeDto): number => value.parentId)
        )

        //let v3625 = this.valuesMap.get(3625)
        //console.log("3625: ", v3625)
        //console.log("TreeSericeImpl.loadData - this.valuesMap: ", this.valuesMap)
        //console.log("TreeSericeImpl.loadData - this.parentsMap: ", this.parentsMap)
    }

    getChildren = (parentId: number): Promise<ITreeNodeDto[]> =>
    {
        return new Promise<ITreeNodeDto[]>((resolve: (value: ITreeNodeDto[]) => void, reject: (reason?: any) => void): void =>
        {
            setTimeout(() => 
                resolve(this.parentsMap.get(parentId).toArray()),
                Math.random() * 2000 + 1000)            
        })
    }
}