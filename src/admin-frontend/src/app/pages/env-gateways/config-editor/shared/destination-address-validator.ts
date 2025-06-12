import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export class DestinationAddressValidator {
    static validate(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const value = control.value;
            const pattern = /^(https?|ftps?):\/\/(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}(?::(?:0|[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5]))?(?:\/(?:[-a-zA-Z0-9@%_\+.~#?&=]+\/?)*)?$/;

            // Emty string is allowed
            if (!value || value.trim().length === 0) {
                return null;
            }

            if (!pattern.test(value)) {
                return {
                    invalidDestinationAddress: {
                        message: 'Please enter a valid URL (http/https/ftp/ftps with domain and optional port/path)',
                        actualValue: value
                    }
                };
            }

            return null;
        }
    }
}