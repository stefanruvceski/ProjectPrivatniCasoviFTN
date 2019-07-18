
export class TeacherSubject {
    constructor(
                public Subjects: Array<SubjectByType>) {}
}

export class SubjectByType {
    constructor(
                public Subjects: Array<SubjectTeach>,
                public Name: string) {}
}

export class SubjectTeach {
    constructor(
                public Name:string,
                public Details: string,
                public Type:string) {}
}
