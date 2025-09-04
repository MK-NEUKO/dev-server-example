import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export class NameValidator {
    static validate(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const value = control.value as string;
            const pattern = /^[a-zA-Z0-9-]+$/;

            if (!value || value.trim().length === 0) {
                return null;
            }

            if (!pattern.test(value)) {
                return {
                    invalidNameProperty: {
                        message: 'Invalid name: Only letters (a-z, A-Z), digits (0-9), and hyphens (-) are allowed. No spaces or special characters.',
                        actualValue: value
                    }
                };
            }

            return null;
        };
    }
}