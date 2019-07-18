export class Subject {
    constructor(
                public Name: string,
                public Details: string,
                public Teachers: Array<Teacher>,
               ) {}
}

export class Teacher {
    constructor(
                public FullName: string,
                public Image: string,
                public Email: string) {}
}
