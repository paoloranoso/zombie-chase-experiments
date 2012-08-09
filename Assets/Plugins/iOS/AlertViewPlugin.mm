#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern UIViewController *UnityGetGLViewController();

#pragma mark Plug-in Function

extern "C" void _ShowMessage(const char *title, const char *message) {
    
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:[NSString stringWithUTF8String:title] message:[NSString stringWithUTF8String:message] delegate:nil cancelButtonTitle:@"Cancel" otherButtonTitles:nil, nil];
    [alertView show];        
    [alertView release];
}


extern "C" void _ShowConfirmMessage(const char *title, const char *message, const char *confirmText) {
    
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:[NSString stringWithUTF8String:title] message:[NSString stringWithUTF8String:message] delegate:nil cancelButtonTitle:@"Cancel" otherButtonTitles:[NSString stringWithUTF8String:confirmText], nil];
    [alertView show]; 
    [alertView release];
}
