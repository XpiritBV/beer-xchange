export interface Beer {
    id: number;
    name: string;
    brewery: string;
    country: string;
    createdBy: string;
    picture: string;
    addedDate: Date;
    removedDate: Date;
    removedBy: string;
    switchedForId: number;
}