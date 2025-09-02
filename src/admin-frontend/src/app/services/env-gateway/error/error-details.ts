export class ErrorDetails {
    constructor(
        public title: string = "unknown",
        public type: string = "unknown",
        public status: string = "unknown",
        public detail: string = "unknown",
        public errors: Record<string, string>[] = []
    ) { }
}