
export const uuidv4 = () : string =>
{
    return ('' + [1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, ch =>
        {
            let c = Number(ch);
            return (c ^ (crypto.getRandomValues(new Uint8Array(1)) as Uint8Array)[0] & 15 >> c / 4).toString(16);
        })              
}