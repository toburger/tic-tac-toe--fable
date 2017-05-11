namespace Fable.Import
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS
open Fable.Import.React

[<Erase>]
module Recompose =
    [<Import("onlyUpdateForKeys", "recompose")>]
    let onlyUpdateForKeys (propKeys: string list) (x: ReactElement): ReactElement = jsNative

    [<Import("pure", "recompose")>]
    let ``pure``: ReactElement = jsNative

    [<Import("branch", "recompose")>]
    let branch (test: obj -> bool, left: unit -> ReactElement, right: unit -> ReactElement) = jsNative

// module recompose =
//     type [<Import("*", "recompose")>] Globals =
//         static member onlyUpdateForKeys(propKeys: string list, x: 'a): 'a = jsNative

// module recompose =
//     type [<AllowNullLiteral>] Component<'P> =
//         U2<ComponentClass<'P>, StatelessComponent<'P>>

//     and [<AllowNullLiteral>] mapper<'TInner, 'TOutter> =
//         Func<'TInner, 'TOutter>

//     and [<AllowNullLiteral>] predicate<'T> =
//         mapper<'T, bool>

//     and [<AllowNullLiteral>] predicateDiff<'T> =
//         Func<'T, 'T, bool>

//     and [<AllowNullLiteral>] Subscribable<'T> =
//         abstract subscribe: Function with get, set

//     and [<AllowNullLiteral>] ComponentEnhancer<'TInner, 'TOutter> =
//         [<Emit("$0($1...)")>] abstract Invoke: ``component``: Component<'TInner> -> ComponentClass<'TOutter>

//     and [<AllowNullLiteral>] InferableComponentEnhancer =
//         [<Emit("$0($1...)")>] abstract Invoke: ``component``: 'TComp -> 'TComp

//     and [<AllowNullLiteral>] EventHandler =
//         Function

//     and [<AllowNullLiteral>] HandleCreators<'TOutter> =
//         obj

//     and [<AllowNullLiteral>] HandleCreatorsFactory<'TOutter> =
//         Func<'TOutter, HandleCreators<'TOutter>>

//     and [<AllowNullLiteral>] NameMap =
//         obj

//     and [<AllowNullLiteral>] reducer<'TState, 'TAction> =
//         Func<'TState, 'TAction, 'TState>

//     and [<AllowNullLiteral>] ReactLifeCycleFunctions =
//         abstract componentWillMount: Function option with get, set
//         abstract componentDidMount: Function option with get, set
//         abstract componentWillReceiveProps: Function option with get, set
//         abstract shouldComponentUpdate: Function option with get, set
//         abstract componentWillUpdate: Function option with get, set
//         abstract componentDidUpdate: Function option with get, set
//         abstract componentWillUnmount: Function option with get, set

//     and [<AllowNullLiteral>] componentFactory =
//         Func<obj, React.ReactNode, React.ReactElement<obj>>

//     and [<AllowNullLiteral>] EventHandlerOf<'T, 'TSubs> =
//         obj

//     and [<AllowNullLiteral>] ObservableConfig =
//         obj

//     type [<Import("*","recompose")>] Globals =
//         static member renderNothing with get(): InferableComponentEnhancer = jsNative and set(v: InferableComponentEnhancer): unit = jsNative
//         static member onlyUpdateForPropTypes with get(): InferableComponentEnhancer = jsNative and set(v: InferableComponentEnhancer): unit = jsNative
//         static member toClass with get(): InferableComponentEnhancer = jsNative and set(v: InferableComponentEnhancer): unit = jsNative
//         static member mapProps(propsMapper: mapper<'TOutter, 'TInner>): ComponentEnhancer<'TInner, 'TOutter> = jsNative
//         static member withProps(createProps: U2<'TInner, mapper<'TOutter, 'TInner>>): ComponentEnhancer<obj, 'TOutter> = jsNative
//         static member withPropsOnChange(shouldMapOrKeys: U2<ResizeArray<string>, predicateDiff<'TOutter>>, createProps: mapper<'TOutter, 'TInner>): ComponentEnhancer<obj, 'TOutter> = jsNative
//         static member withHandlers(handlerCreators: U2<HandleCreators<'TOutter>, HandleCreatorsFactory<'TOutter>>): ComponentEnhancer<'TInner, 'TOutter> = jsNative
//         static member defaultProps(props: obj): InferableComponentEnhancer = jsNative
//         static member renameProp(outterName: string, innerName: string): ComponentEnhancer<obj, obj> = jsNative
//         static member renameProps(nameMap: NameMap): ComponentEnhancer<obj, obj> = jsNative
//         static member flattenProp(propName: string): ComponentEnhancer<obj, obj> = jsNative
//         static member withState(stateName: string, stateUpdaterName: string, initialState: U2<obj, mapper<'TOutter, obj>>): ComponentEnhancer<'TOutter, 'TOutter> = jsNative
//         static member withReducer(stateName: string, dispatchName: string, reducer: reducer<'TState, 'TAction>, initialState: 'TState): ComponentEnhancer<obj, obj> = jsNative
//         static member withReducer(stateName: string, dispatchName: string, reducer: reducer<'TState, 'TAction>, initialState: Func<'TOutter, 'TState>): ComponentEnhancer<obj, 'TOutter> = jsNative
//         static member branch(test: predicate<'TOutter>, trueEnhancer: InferableComponentEnhancer, ?falseEnhancer: InferableComponentEnhancer): ComponentEnhancer<obj, 'TOutter> = jsNative
//         static member renderComponent(``component``: U2<string, Component<obj>>): ComponentEnhancer<obj, obj> = jsNative
//         static member shouldUpdate(test: predicateDiff<'TProps>): InferableComponentEnhancer = jsNative
//         static member ``pure``(``component``: 'TComp): 'TComp = jsNative
//         static member onlyUpdateForKeys(propKeys: ResizeArray<string>): InferableComponentEnhancer = jsNative
//         static member withContext(childContextTypes: ValidationMap<'TContext>, getChildContext: mapper<'TProps, obj>): InferableComponentEnhancer = jsNative
//         static member getContext(contextTypes: ValidationMap<'TContext>): InferableComponentEnhancer = jsNative
//         static member lifecycle(spec: ReactLifeCycleFunctions): InferableComponentEnhancer = jsNative
//         static member setStatic(key: string, value: obj): ComponentEnhancer<'TOutter, 'TOutter> = jsNative
//         static member setPropTypes(propTypes: ValidationMap<'TOutter>): ComponentEnhancer<obj, 'TOutter> = jsNative
//         static member setDisplayName(displayName: string): ComponentEnhancer<'TOutter, 'TOutter> = jsNative
//         static member compose([<ParamArray>] functions: Function[]): ComponentEnhancer<'TInner, 'TOutter> = jsNative
//         static member getDisplayName(``component``: Component<obj>): string = jsNative
//         static member wrapDisplayName(``component``: Component<obj>, wrapperName: string): string = jsNative
//         static member shallowEqual(a: obj, b: obj): bool = jsNative
//         static member isClassComponent(value: obj): bool = jsNative
//         static member createEagerElement(``type``: U2<Component<obj>, string>, ?props: obj, ?children: React.ReactNode): React.ReactElement<obj> = jsNative
//         static member createEagerFactory(``type``: U2<Component<obj>, string>): componentFactory = jsNative
//         static member createSink(callback: Func<obj, unit>): React.ComponentClass<obj> = jsNative
//         static member componentFromProp(propName: string): StatelessComponent<obj> = jsNative
//         static member nest([<ParamArray>] Components: U2<string, Component<obj>>[]): React.ComponentClass<obj> = jsNative
//         static member hoistStatics(hoc: InferableComponentEnhancer): InferableComponentEnhancer = jsNative
//         static member componentFromStream(propsToReactNode: mapper<Subscribable<'TProps>, Subscribable<React.ReactNode>>): Component<'TProps> = jsNative
//         static member mapPropsStream(transform: mapper<Subscribable<'TOutter>, Subscribable<'TInner>>): ComponentEnhancer<'TInner, 'TOutter> = jsNative
//         static member createEventHandler(): EventHandlerOf<'T, 'TSubs> = jsNative
//         static member setObservableConfig(config: ObservableConfig): unit = jsNative



// module ``recompose/rxjsObservableConfig`` =
//     type [<Import("*","recompose/rxjsObservableConfig")>] Globals =
//         static member rxjsconfig with get(): ObservableConfig = jsNative and set(v: ObservableConfig): unit = jsNative



// module ``recompose/rxjs4ObservableConfig`` =
//     type [<Import("*","recompose/rxjs4ObservableConfig")>] Globals =
//         static member rxjs4config with get(): ObservableConfig = jsNative and set(v: ObservableConfig): unit = jsNative



// module ``recompose/mostObservableConfig`` =
//     type [<Import("*","recompose/mostObservableConfig")>] Globals =
//         static member mostConfig with get(): ObservableConfig = jsNative and set(v: ObservableConfig): unit = jsNative



// module ``recompose/xstreamObservableConfig`` =
//     type [<Import("*","recompose/xstreamObservableConfig")>] Globals =
//         static member xstreamConfig with get(): ObservableConfig = jsNative and set(v: ObservableConfig): unit = jsNative



// module ``recompose/baconObservableConfig`` =
//     type [<Import("*","recompose/baconObservableConfig")>] Globals =
//         static member baconConfig with get(): ObservableConfig = jsNative and set(v: ObservableConfig): unit = jsNative



// module ``recompose/kefirObservableConfig`` =
//     type [<Import("*","recompose/kefirObservableConfig")>] Globals =
//         static member kefirConfig with get(): ObservableConfig = jsNative and set(v: ObservableConfig): unit = jsNative


